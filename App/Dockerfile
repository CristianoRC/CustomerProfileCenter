FROM node:lts-alpine as build
ARG ENV=dev
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build-$ENV

FROM nginx:stable-alpine
COPY --from=build /app/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]