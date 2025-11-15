CREATE TABLE users (
    Id SERIAL PRIMARY KEY,
    Code VARCHAR(255) NOT NULL,
    Email TEXT UNIQUE
);

CREATE TABLE texts (
    id SERIAL PRIMARY KEY,
    user_id INTEGER NOT NULL REFERENCES users(id) ON DELETE CASCADE,
    content TEXT NOT NULL,
    title TEXT NOT NULL,
    language_from TEXT NOT NULL,
    language_to TEXT NOT NULL
);
CREATE TABLE words (
    id SERIAL PRIMARY KEY,
    text_id INTEGER NOT NULL REFERENCES texts(id) ON DELETE CASCADE,
    user_code INTEGER NOT NULL REFERENCES users(code) ON DELETE CASCADE,
    word TEXT NOT NULL,
    is_pinned BOOLEAN NOT NULL DEFAULT false,
    level TEXT NULL
);
