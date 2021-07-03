![Thumbnail](./Assets/Logo.png)

# AlinSpace.Database
AlinSpace database contracts, repositories, docker container for PostgreSQL database, and additional scripts for maintenance.

# How to migrate the database
Start the **docker-compose.yml**. This docker-compose file will start the **PostgreSQL database** in a docker container (alinspace-database), and 
build the **AlinSpace.Database** project in another docker container (alinspace-database-shell).

When the two containers are running you can run the following code to gain access to the alinspace-database-shell:

```
docker exec -it alinspace-database-shell /bin/bash
```

In this shell you can migrate the database. The PostgrSQL server is reachable by the hostname **alinspace-database**.

# Database data
The database data for **alinspace-database** will be stored in the folder **/database-data**.
For database backups simply backup the content of the database data folder.
If you want to alter the database folder location, simply update the **docker-compose.yml** file.

