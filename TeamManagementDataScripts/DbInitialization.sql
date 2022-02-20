CREATE Database Club;

create schema clubmanager;

CREATE User team_manager with password 'apple445!!!@them';

Alter team_manager Set search_path to clubmanager,clubhistory,clubstats;

GRANT ALL PREVILEGES ON clubmanager TO team_manager;
GRANT SELECT,DELETE,UPDATE,INSERT ON ALL TABLES IN SCHEMA clubmanager TO team_manager;
GRANT ALL EXECUTE ON ALL FUNCTDIONS IN SCHEMA clubmanager TO team_manager;
GRANT ALL PREVILEGES ON SEQUENCES IN SCHEMA clubmanager TO team_manager