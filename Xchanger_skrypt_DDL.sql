CREATE TABLE Categories (
    id int  NOT NULL IDENTITY(1, 1),
    name varchar(20)  NOT NULL,
    CONSTRAINT Categories_pk PRIMARY KEY  (id)
);


CREATE TABLE Exchanges (
    id int  NOT NULL IDENTITY(1, 1),
    accept_date datetime  NOT NULL,
    reply_date datetime  NOT NULL,
    request_date datetime  NOT NULL,
    state tinyint  NOT NULL,
    item_id int  NOT NULL,
    item_2_id int  NULL,
    initiator_id int  NOT NULL,
    mess_1 nvarchar(180)  NOT NULL,
    mess_2 nvarchar(180)  NOT NULL,
    CONSTRAINT Exchanges_pk PRIMARY KEY  (id)
);


CREATE TABLE Items (
    id int  NOT NULL IDENTITY(1, 1),
    title nvarchar(60)  NOT NULL,
    description nvarchar(1700)  NOT NULL,
    publication_date date  NOT NULL,
    is_new bit  NOT NULL,
    category_id int  NOT NULL,
    user_id int  NOT NULL,
    active bit  NOT NULL,
    location nvarchar(60)  NOT NULL,
    CONSTRAINT Items_pk PRIMARY KEY  (id)
);

CREATE TABLE Users (
    id int  NOT NULL IDENTITY(1, 1),
    login nvarchar(40)  NOT NULL,
    email nvarchar(60)  NOT NULL,
    phone_number char(9)  NULL,
    password_hash varbinary(128)  NOT NULL,
    password_salt varbinary(64)  NULL,
    refresh_token varchar(64)  NOT NULL,
    refresh_token_expire datetime  NOT NULL,
    refresh_token_create datetime  NOT NULL,
    join_date date  NOT NULL,
    CONSTRAINT Id PRIMARY KEY  (id)
);


ALTER TABLE Exchanges ADD CONSTRAINT Exchanges_Items1
    FOREIGN KEY (item_id)
    REFERENCES Items (id);

ALTER TABLE Exchanges ADD CONSTRAINT Exchanges_Items2
    FOREIGN KEY (item_2_id)
    REFERENCES Items (id);

ALTER TABLE Exchanges ADD CONSTRAINT Exchanges_Users
    FOREIGN KEY (initiator_id)
    REFERENCES Users (id);

ALTER TABLE Items ADD CONSTRAINT Items_Categories
    FOREIGN KEY (category_id)
    REFERENCES Categories (id);

ALTER TABLE Items ADD CONSTRAINT Items_Users
    FOREIGN KEY (user_id)
    REFERENCES Users (id);