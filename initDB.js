use admin;
db.adminCommand({authSchemaUpgrade: 1});
db.createUser(
    {
        user: "siteUserAdmin",
        pwd: "unPasswordQuiVaBien",
        roles: [{ role: "userAdminAnyDatabase", db: "admin" }]
    }
);
use angularmovies;
db.createUser(
    {
        user: "angularUser",
        pwd: "password",
        roles: ["dbOwner"]
    }
);
db.createCollection("movies");
