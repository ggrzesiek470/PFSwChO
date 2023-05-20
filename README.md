# PFSwChO
docker build -t pfswcho .

docker run -p 8080:80 pfswcho

docker ps -a

docker logs (wstawić container_id z docker ps -a) 

docker history pfswcho:dev
