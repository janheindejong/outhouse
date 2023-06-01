#!/bin/bash

set -e

rm ./data/db.sqlite 
cat sql/create_test_db.sql | sqlite3 data/db.sqlite
