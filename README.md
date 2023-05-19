# PFSwChO
docker build -t pfswcho:dev .

docker run -p 8080:80 pfswcho:dev

docker ps -a

docker logs (wstawić container_id z docker ps -a) 

docker history pfswcho:dev
