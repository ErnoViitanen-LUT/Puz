#!/bin/sh

# Credits: http://stackoverflow.com/a/750191

git filter-branch -f --env-filter "
    GIT_AUTHOR_NAME='Erno Viitanen'
    GIT_AUTHOR_EMAIL='erno.viitanen@student.lut.fi'
    GIT_COMMITTER_NAME='Erno Viitanen'
    GIT_COMMITTER_EMAIL='erno.viitanen@student.lut.fi'
  " HEAD
