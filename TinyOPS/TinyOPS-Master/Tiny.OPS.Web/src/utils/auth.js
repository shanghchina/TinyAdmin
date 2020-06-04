import Cookies from 'js-cookie'
import Config from '@/settings'

var inFifteenMinutes = new Date(new Date().getTime() + 120 * 60 * 1000)
const LoginIdKey = 'poc_login_id'
const TokenKey = Config.TokenKey
// 原sso处理逻辑
export function getLoginId() {
  return Cookies.get(LoginIdKey)
}
export function setLoginId(vaule) {
  return Cookies.set(LoginIdKey, vaule, {
    expires: inFifteenMinutes
  })
}
export function removeLoginId() {
  return Cookies.remove(LoginIdKey)
}

// =======================  const TokenKey = Config.TokenKey

export function getToken() {
  return Cookies.get(TokenKey)
}

export function setToken(token, rememberMe) {
  if (rememberMe) {
    return Cookies.set(TokenKey, token, { expires: Config.tokenCookieExpires })
  } else return Cookies.set(TokenKey, token)
}

export function removeToken() {
  return Cookies.remove(TokenKey)
}
