import Cookies from 'js-cookie'
var inFifteenMinutes = new Date(new Date().getTime() + 120 * 60 * 1000)
const LoginIdKey = 'poc_login_id'

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

