CREATE TABLE user (
    id INTEGER PRIMARY KEY, 
    name TEXT NOT NULL,
    email TEXT NOT NULL UNIQUE
);

INSERT INTO user (name, email)
VALUES 
    ('Piet', 'piet@comp.com'),
    ('Kees', 'kees@comp.com');
