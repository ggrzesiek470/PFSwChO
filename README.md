# PFSwChO
docker build -t pfswcho .

docker run -p 8080:80 pfswcho

docker ps -a

docker logs (wstawić container_id z docker ps -a) 

docker history pfswcho:dev

Logi aplikacji

![logs_aplication](https://github.com/ggrzesiek470/PFSwChO/assets/72609123/674e5b00-209b-4657-92f4-52b261021ed3)


Logi z docker

![logs_docker_desktop](https://github.com/ggrzesiek470/PFSwChO/assets/72609123/4c2f75c7-9455-4b63-9bc0-88d1a45a1ff0)
