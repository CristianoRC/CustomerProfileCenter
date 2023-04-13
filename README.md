# Customer Profile Center

<img src="https://i.imgur.com/YCKVzCE.png" width="50%">

![App workflow](https://github.com/CristianoRC/CustomerProfileCenter/actions/workflows/azure-staticwebapp.yml/badge.svg)

## Documentação

### Diagramas

- [System Context](./Docs/1%20-%20System%20Context/)
- [Container](./Docs/2%20-%20Container/)
- [Component](./Docs/3%20-%20Componente/)
- [Infra](./Docs/Infra/)

### Executar projeto localmente

Para executr o projeto basta rodar na raiz do projeto o command: `docker-compose up --build`

Temos apenas um ponto de atenção, se você usa processadores `arm64` pode ter problemas por causa da imagem Docker de Azure Functions, usada em uma parte do projeto. Para mais detalhes você pode encontrar aqui [Mac M1 Function Not Implemented Error](https://github.com/docker/for-mac/issues/5328).
Se necessário o uso com Docker em processadores arm64, pode entrar em contato que podemos criar uma imagem base compatível.

#### **Acessar a aplicação WEB do container localmente**

http://localhost.cristianoprogramador.com/


### Como acessar o projeto no Azure

<strike>Se quiseres acessar o projeto diretamente na Web, na landing page que foi criada, basta acessar [https://customerprofile.cristianoprogramador.com/](https://customerprofile.cristianoprogramador.com/). Os serviços estão hospedados em um ambiente no Azure.</strike> Ambiente de back-end desativado temporariamente por causa dos gastos, se necessário pode entrar em contato que ativo novamente.


#### **Requisições que podem ser feritas**

- Obter informações do CEP: HTTP GET - http://localhost:5021/Address/{cep}
- Cadastro de Cliente: HTTP POST - http://localhost:5021/Customer

```json
{
  "name": "Cristiano Cunha",
  "document": {
    "number": "902.522.750-39",
    "documentType": 0
  },
  "address": {
    "cep": "01025020",
    "number": "123",
    "complement": "N/A"
  },
  "birthday": "1998-07-28T13:07:56.816Z",
  "corporateName": "",
  "emailAddress": "contato@cristianoprogramador.com",
  "phoneNumber": "53984319169"
}
```

Obs.:

`"documentType": 0, //0 CPF, 1 CNPJ`

`CEP é apenas de exmeplo gerado aleatoriamente, não usar valores reais`
