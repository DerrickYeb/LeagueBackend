SET SEARCH_PATH TO clubmanager,clubhistory,clubstats,clubgifts;
CREATE OR REPLACE FUNCTION add_new_team(
_team_code CHARACTER VARYING(1000),
_team_name CHARACTER VARYING(1000),
_slogan CHARACTER VARYING(1000),
_email CHARACTER VARYING(500),
_website CHARACTER VARYING(500),
_password TEXT,
_division_type_id INTEGER,
_short_hand CHARACTER VARYING(10),
_phone_number CHARACTER VARYING(20),
_created_date TIMESTAMP WITHOUT TIME ZONE)

RETURNS TABLE(
"NewTeamId" BIGINT,
"TeamCode" CHARACTER VARYING(1000),
"ClubName" CHARACTER VARYING(1000),
"Slogan" CHARACTER VARYING(1000),
"ShortHand" CHARACTER VARYING(10),
"DivisionTypeId" INTEGER,
"Email" CHARACTER VARYING(500),
"PhoneNumber" CHARACTER VARYING(20),
"Website" CHARACTER VARYING(500)
)

LANGUAGE 'plpgsql'
AS
$$
	DECLARE newTeamId BIGINT;
BEGIN
	INSERT INTO "Team"("TeamCode","ShortHand","ClubName","Slogan","Email","PhonNumber","Website",
					 "DivisionTypeId","CreatedDate")
					 VALUES(_team_code,_short_hand,_club_name,_slogan,_email,_phone_number,_website,
						   _division_type_id,_created_date)
					RETURNING "TeamId" INTO newTeamId;
					
			IF newTeamId IS NOT NULL THEN
			
			INSERT INTO "TeamLogin"("TeamId","Email","Password","PhoneNumber","CreatedDate")
							VALUES(newTeamId,_email,_password,_phone_number,_created_date);
			END IF;
		RETURN QUERY
			SELECT 
				t."NewTeamId" "TeamId",
				t."TeamCode",
				t."ClubName",
				t."Slogan",
				t."ShortHand",
				t."DivisionTypeId",
				t."Email",
				t."PhoneNumber",
				t."Website",
				t."CreatedDate"
			FROM "Team" t WHERE t."TeamId" = newTeamId;
END
$$

CREATE OR REPLACE FUNCTION add_team_image(
_imageblob CHARACTER VARYING(1000),
_image_url CHARACTER VARYING(1000),
_team_id BIGINT,
_team_code CHARACTER VARYING(500),
_created_date TIMESTAMP WITHOUT TIME ZONE)

RETURNS TABLE(
"TeamCode" CHARACTER VARYING(1000),
"ImageUrl" TEXT,
"ImageBlob" TEXT
)
LANGUAGE 'plpgsql'
AS
$$
DECLARE newImgId BIGINT;
DECLARE existingTeamId BIGINT;
	BEGIN
		SELECT t."TeamId"
				FROM "TeamImage" t WHERE t."TeamId" = _team_id INTO existingTeamId;
	IF existingTeamId IS NOT NULL THEN
		UPDATE "TeamImage" SET
				"ImageUrl" = _image_url,
				"ImageBlob" = _image_blob
				WHERE "TeamId" = _team_id;
			RETURN QUERY
				SELECT img."TeamCode",
						img."ImageUrl",
						img."ImageBlob"
					FROM "TeamImage" img WHERE img."TeamId" = existingTeamImage;
	ELSE
		INSERT INTO "TeamImage"("TeamId","ImageUrl","ImageBlob","CreatedDate")
			VALUES(_team_id,_image_url,_image_blob,_created_date)
			RETURNING "ImageId" INTO newImgId;
			
		IF newImgId IS NOT NULL THEN
		
		RETURN QUERY
			SELECT 
			img."TeamCode",
			img."ImageUrl",
			img."ImageBlob"
		FROM "TeamImage" img WHERE img."ImageId" = newImgId; 
		END IF;
		END IF;
	END

CREATE OR REPLACE FUNCTION login_with_email_and_password(
_email CHARACTER VARYING(20),
_password TEXT)

RETURNS TABLE(
"Email" CHARACTER VARYING(500),
"PhoneNumber" CHARACTER VARYING(500),
"TeamId" BIGINT,
"IsLoggedIn" BOOLEAN,
"LastLoginDate" TIMESTAMP WITHOUT TIME ZONE)

LANGUAGE 'plpgsql'
VOLATILE
AS
$$
	DECLARE existTeamLogId BIGINT;
BEGIN
	SELECT tlo."TeamLoginId"
		FROM "TeamLogin" tlo WHERE tlo."Email" = _email and tlo."Password" = _password LIMIT 1
		INTO	existTeamLogId;
		IF existTeamLogId IS NOT NULL THEN
		--Update Login details
		UPDATE "TeamLogin" SET "IsLoggedIn"=TRUE,"LastLoginDate"=CURRENT_TIMESTAMP WHERE "TeamLoginId"=existTeamLogId;
		RETURN QUERY
			SELECT
				tl."Email",
				tl."PhoneNumber",
				tl."TeamId",
				tl."IsLoggedIn",
				tl."LastLoginDate"
			FROM "TeamLogin" tl
			WHERE tl."TeamLoginId" = existTeamLogId;

	END IF;
  END;
$$;