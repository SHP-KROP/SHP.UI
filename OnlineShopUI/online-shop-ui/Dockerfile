FROM node:lts as build-stage
WORKDIR /app
COPY package*.json ./
RUN npm install -g npm@latest
RUN npm install --legacy-peer-deps
COPY . .
RUN npm run build

FROM nginx:latest
COPY --from=build-stage /app/build /usr/share/nginx/html
EXPOSE 3000
CMD ["npm", "start"]