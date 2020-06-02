
import { Message } from 'element-ui'
const getUrlKey = (name) => {
  var reg = new RegExp('(^|&)' + name + '=([^&]*)(&|$)', 'i') // 定义正则表达式
  var r = window.location.search.substr(1).match(reg)
  if (r != null) return unescape(r[2])
  return null
}

const getTransactionID = () => {
  var rand = ''
  for (var i = 0; i < 4; i++) {
    var r = Math.floor(Math.random() * 10)
    rand += r
  }
  const date = new Date()
  const month = (date.getMonth() + 1).toString().padStart(2, '0')
  const strDate = date.getDate().toString().padStart(2, '0')
  const hour = date.getHours().toString().padStart(2, '0')
  const minute = date.getMinutes().toString().padStart(2, '0')
  const second = date.getSeconds().toString().padStart(2, '0')
  const milliseconds = date.getSeconds().toString().padStart(3, '00')
  return `${date.getFullYear()}${month}${strDate}${hour}${minute}${second}${milliseconds}` + rand
}

const getRequestDate = () => {
  const date = new Date()
  const month = (date.getMonth() + 1).toString().padStart(2, '0')
  const strDate = date.getDate().toString().padStart(2, '0')
  const hour = date.getHours().toString().padStart(2, '0')
  const minute = date.getMinutes().toString().padStart(2, '0')
  const second = date.getSeconds().toString().padStart(2, '0')
  return `${date.getFullYear()}${month}${strDate}${hour}${minute}${second}`
}

const toast = (message, type) => {
  Message({
    duration: 1500,
    message: message,
    type: type
  })
}

function cartesianProductOf() {
  return Array.prototype.reduce.call(arguments, function(a, b) {
    var ret = []
    a.forEach(function(a) {
      b.forEach(function(b) {
        ret.push(a.concat([b]))
      })
    })
    return ret
  }, [[]])
}

/*eslint no-extend-native: ["error", { "exceptions": ["Date"] }]*/
Date.prototype.Format = function(fmt) {
  var o = {
    'M+': this.getMonth() + 1, // 月份
    'd+': this.getDate(), // 日
    'H+': this.getHours(), // 小时
    'm+': this.getMinutes(), // 分
    's+': this.getSeconds(), // 秒
    'q+': Math.floor((this.getMonth() + 3) / 3), // 季度
    'S': this.getMilliseconds() // 毫秒
  }
  if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + '').substr(4 - RegExp.$1.length))
  for (var k in o) { if (new RegExp('(' + k + ')').test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (('00' + o[k]).substr(('' + o[k]).length))) }
  return fmt
}

export default {
  getRequestDate,
  getTransactionID,
  getUrlKey,
  toast,
  cartesianProductOf
}
