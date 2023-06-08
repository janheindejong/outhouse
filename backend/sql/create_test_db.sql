CREATE TABLE user (
    id INTEGER PRIMARY KEY, 
    name TEXT NOT NULL,
    email TEXT NOT NULL UNIQUE
);

INSERT INTO user (name, email)
VALUES 
    ('Piet', 'piet@comp.com'),
    ('Kees', 'kees@comp.com');

CREATE TABLE cottage (
    id INTEGER PRIMARY KEY, 
    name TEXT NOT NULL
);

INSERT INTO cottage (name)
VALUES 
    ('Duinzicht'), 
    ('Veldwacht');

CREATE TABLE memberships (
    id INTEGER PRIMARY KEY, 
    user_id INT NOT NULL,
    cottage_id INT NOT NULL, 
    role TEXT CHECK( role IN ('admin','member') ) NOT NULL,
    FOREIGN KEY(user_id) REFERENCES user(id),
    FOREIGN KEY(cottage_id) REFERENCES cottage(id),
    UNIQUE(user_id, cottage_id)
);

INSERT INTO memberships (user_id, cottage_id, role)
VALUES 
    (1, 1, 'admin'),
    (1, 2, 'member'),
    (2, 2, 'admin');
