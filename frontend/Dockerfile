FROM node:20
# Installing libvips-dev for sharp Compatibility
RUN apt-get update && apt-get install -y build-essential gcc autoconf automake zlib1g-dev libpng-dev nasm bash libvips-dev git
ARG NODE_ENV=development
ENV NODE_ENV=${NODE_ENV}
WORKDIR /tmb-oms/
COPY package.json package-lock.json ./
RUN npm install -g node-gyp
RUN npm config set fetch-retry-maxtimeout 600000 -g && npm install
ENV PATH /tmb-oms/node_modules/.bin:$PATH
WORKDIR /tmb-oms/frontend
COPY . .
RUN chown -R node:node /tmb-oms/frontend
USER node
EXPOSE 3000
ENV PORT 3000
CMD ["sh", "-c", "if [ \"$NODE_ENV\" = \"development\" ]; then npm run dev; else npm run build && npm run start; fi"]
