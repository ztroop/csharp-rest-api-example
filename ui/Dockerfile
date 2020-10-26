FROM node:lts-alpine

WORKDIR /app
ENV PATH /app/node_modules/.bin:$PATH

COPY package.json ./package.json
COPY yarn.lock ./yarn.lock

RUN yarn install

COPY public ./public
COPY src ./src

CMD yarn start
