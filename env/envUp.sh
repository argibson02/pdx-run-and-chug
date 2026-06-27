#!/bin/bash

CONTAINER="runclub-mariadb"
ROOT_PASSWORD="password"

if podman ps -q -f name=$CONTAINER | grep -q .; then
  echo "$CONTAINER is already running"
  exit 0
fi

if podman ps -aq -f name=$CONTAINER | grep -q .; then
  echo "Starting existing $CONTAINER container"
  podman start $CONTAINER
  exit 0
fi

echo "Starting new $CONTAINER container..."
podman run --detach \
  --name $CONTAINER \
  --env MARIADB_ROOT_PASSWORD="$ROOT_PASSWORD" \
  -p 3306:3306 mariadb:latest

echo "Waiting for MariaDB to be ready..."
for i in {1..10}; do
  podman exec $CONTAINER //usr//local//bin//healthcheck.sh --connect 2>/dev/null && break
  [ $i -eq 10 ] && echo "Timed out waiting for MariaDB" && exit 1
  sleep 2
done

echo "Seeding database..."
podman exec -i $CONTAINER mariadb --user=root --password="$ROOT_PASSWORD" < backend/Data/seed.sql
echo "Done."
