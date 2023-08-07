
# Crawler

Uma breve aplicação desenvolvida em .NET 6 que  recebe um json contendo um numero de cpf e credenciais de login do portal [ExtratoClube](http://extratoclube.com.br/), processa o cpf e verifica se há um número de benefiíco cadastrado para o documento. Esse processamento tem o seguinte fluxo: Recebe o json com as informações, retorna uma resposta informando que era ser processado, coloca o cpf em uma fila e verifica quais dados estão vinculados aquele cpf, salvando essas informações em cache e indexando no Elasticsearch. Depois é possiível verficar no endpoint de consulta.



## Instalação e configurações de ambiente

Para executar o projeto, é necessário ter instalado uma o [Docker]( https://docs.docker.com/compose/install/) e a [versão 6 do .NET](https://dotnet.microsoft.com/pt-br/download/dotnet/6.0) para o backend e o [Node](https://nodejs.org/pt-br/download) para o frontend. Após obter a versão correta, faça o clone do repositório para sua máquina.

```bash
git clone https://github.com/thiagoaure/crawler-extratoclube
```


## Run

Para executar o projeto sem nenhum problema de conexão, execute o container docker onde estão as imagens utilizadas

```
docker-compose up
```
Após executar o docker, vamos inicar o back-end da aplicação:
- Na pasta \backend\Application.Crawler.ExtratoClube\Application.Crawler.ExtratoClube, no terminal, execute os comandos:
```
dotnet restore
```
```
dotnet build
```
```
dotnet run
```

Após finalizar a compilação, está sendo usado Swagger para documentação, onde também pode ser utilizado para fazer as requisições

- Na pasta frontend, abra o terminal, e execute o comando para executar o client da aplicação:
```
npm install
```
```
npm start
```
## Referência



