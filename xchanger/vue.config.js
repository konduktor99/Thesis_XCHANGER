const { defineConfig } = require('@vue/cli-service')
module.exports = defineConfig({
  publicPath: process.env.NODE_ENV === 'production'
    ? ''
    : '/',

    devServer: {
      proxy: {
        '^/api': {
          target: 'https://localhost:5001',
          changeOrigin: true,
          secure:false,
          pathRewrite: {'^/api': '/api'},
      },

      }
      
    },
  
  transpileDependencies: true,
  
})
