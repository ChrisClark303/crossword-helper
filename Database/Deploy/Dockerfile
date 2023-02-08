FROM postgres as db
WORKDIR /app
COPY ./Deploy/scripts/init.sh /docker-entrypoint-initdb.d
COPY ./crossword-helper.backup ./scripts/crossword-helper.backup
