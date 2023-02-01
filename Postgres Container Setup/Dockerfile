FROM postgres as db
WORKDIR /app
COPY ./scripts/init.sh /docker-entrypoint-initdb.d
COPY ./scripts/crossword-helper.backup ./scripts/crossword-helper.backup
