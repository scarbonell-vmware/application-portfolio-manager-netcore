const { createProxyMiddleware } = require('http-proxy-middleware');
module.exports = function (app) {
  app.use(
    '/api',
    createProxyMiddleware('/api',{
      target: 'https://localhost:7208',
      changeOrigin: true,
    }),
  );
};
