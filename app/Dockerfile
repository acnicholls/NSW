FROM node:latest as build-env

WORKDIR /app

COPY ./cra/* /app/

RUN npm install

RUN npm run build -c Production -o dist

FROM nginx:latest as prod

COPY from=build-env /app/dist/* /usr/share/nginx/html/

# copy our configuration file
COPY ./proxy/nginx.dev.conf /etc/nginx/conf.d/default.conf

# copy our includes file
COPY ./proxy/includes.conf /etc/nginx/conf.d/proxy.conf

# expose HTTP port
EXPOSE 80 443

CMD ["nginx", "-g", "daemon off;"]