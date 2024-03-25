CREATE TABLE "Widgets"
(
    "Id" SERIAL PRIMARY KEY,
    "Title" VARCHAR(255),
    "Link" TEXT,
    "Question" TEXT,
    "Html" TEXT DEFAULT '',
    "Color" VARCHAR(50) DEFAULT '#e5e7eb'
);

CREATE TABLE "Nps"
(
    "Id" SERIAL PRIMARY KEY,
    "WidgetId" INT,
    "Answer" TEXT,
    "Rating" INT CHECK ("Rating" >= 0 AND "Rating" <= 10),
    FOREIGN KEY ("WidgetId") REFERENCES "Widgets" ("Id")
);
