import Vue from 'vue'
Vue.filter('formatDate', function(date, fmt) {
  if (!date) {
    return ''
  }

  if (typeof dateStr === 'string') {
    if (date.indexOf('.') > -1) {
      // 有些日期接口返回带有.0。
      date = date.substring(0, date.indexOf('.'))
    }
    // 解决IOS上无法从dateStr parse 到Date类型问题
    date = date.replace(/-/g, '/')
  }

  date = new Date(date)

  if (/(y+)/.test(fmt)) {
    fmt = fmt.replace(RegExp.$1, (date.getFullYear() + '').substr(4 - RegExp.$1.length))
  }
  const o = {
    'M+': date.getMonth() + 1,
    'd+': date.getDate(),
    'h+': date.getHours(),
    'm+': date.getMinutes(),
    's+': date.getSeconds()
  }
  for (const k in o) {
    if (new RegExp(`(${k})`).test(fmt)) {
      const str = o[k] + ''
      fmt = fmt.replace(RegExp.$1, RegExp.$1.length === 1 ? str : padLeftZero(str))
    }
  }
  return fmt
}
)
const padLeftZero = (str) => {
  return ('00' + str).substr(str.length)
}
