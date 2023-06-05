#!/bin/bash

rm ./data/db.sqlite 
cat sql/create_test_db.sqlite | sqlite3 data/db.sqlite
