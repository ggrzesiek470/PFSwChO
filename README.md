# PFSwChO
docker build -t pfswcho .

docker run -p 8080:80 pfswcho

docker ps -a

docker logs (wstawić container_id z docker ps -a) 

docker history pfswcho:dev

Logi aplikacji

![logs_aplication](https://github.com/ggrzesiek470/PFSwChO/assets/72609123/480b3388-0ece-4a93-bc47-c2d018597a7c)

Logi z docker

![logs_docker_desktop](https://github.com/ggrzesiek470/PFSwChO/assets/72609123/4c2f75c7-9455-4b63-9bc0-88d1a45a1ff0)
