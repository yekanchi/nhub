## Wget

### Dump Site as Static Page:
```bash
wget -r -k -E -np --span-hosts --page-requisites -x -l 1 -t 1 https://www.w3schools.com/sql
`