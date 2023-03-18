FROM alpine
RUN wget https://raw.githubusercontent.com/rabbitmq/rabbitmq-management/v3.7.8/bin/rabbitmqadmin
RUN chmod 777 rabbitmqadmin
COPY ./config-rabbitmq.sh .
ENTRYPOINT [ "./config-rabbitmq.sh" ]