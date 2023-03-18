# Customer Profile Center

<img src="https://i.imgur.com/YCKVzCE.png" width="50%">

## Documentação

### Diagramas

- [System Context](./Docs/1%20-%20System%20Context/)

### Como acessar o projeto

Se quiseres acessar o projeto diretamente na Web, na landing page que foi criada, basta acessar [https://customerprofile.cristianoprogramador.com/](https://customerprofile.cristianoprogramador.com/). Os serviços estão hospedados em um ambiente no Azure.

### Executar projeto localmente

Para executr o projeto basta rodar na raiz do projeto o command: `docker-compose up --build`

**Acessar a aplicação WEB do container localmente**

http://localhost.cristianoprogramador.com/

**Requisições que podem ser feritas**

- Obter informações do CEP: HTTP GET - http://localhost:5021/Address/{cep}
- Cadastro de Cliente: HTTP POST - http://localhost:5021/Customer

```json
{
  "name": "Cristiano Cunha",
  "document": {
    "number" : "902.522.750-39",
    "documentType": 0
  },
  "address": {
    "cep": "96085000",
    "number": "2886",
    "complement": "AP 501"
  },
  "birthday": "1998-07-28T13:07:56.816Z",
  "corporateName": "",
  "emailAddress": "contato@cristianoprogramador.com",
  "phoneNumber": "53984319169"
}
```

Obs.:

``"documentType": 0, //0 CPF, 1 CNPJ``

``CEP é apenas de exmeplo gerado aleatoriamente, não usar valores reais``

