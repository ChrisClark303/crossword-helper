FROM postgres:16-alpine as db
WORKDIR /app
COPY ./Deploy/scripts/init.sh /docker-entrypoint-initdb.d
COPY ./crossword-helper.backup ./scripts/crossword-helper.backup
