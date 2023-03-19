echo "Iniciando configuração do RabbitMQ"
./rabbitmqadmin -u $user -p $password -H $host declare exchange name=CreateCustomer type=fanout
./rabbitmqadmin -u $user -p $password -H $host declare queue name=CreateCustomerTopicTrigger
./rabbitmqadmin -u $user -p $password -H $host declare binding source=CreateCustomer destination=CreateCustomerTopicTrigger
echo "Configuração do RabbitMQ Finalizada"