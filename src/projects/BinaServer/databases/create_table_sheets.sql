CREATE TABLE  Sheets (
    letterId integer null,
    sheetId integer null,
    title text null,
    html text null,
    FOREIGN KEY (letterId)
    REFERENCES Letters (ROWID)
       ON UPDATE SET NULL
       ON DELETE SET NULL
)