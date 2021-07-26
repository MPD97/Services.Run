docker build -t service.run . ;
docker tag service.run mateusz9090/run:local ;
docker push mateusz9090/run:local