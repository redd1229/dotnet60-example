// global javaScript code.

const blendColors = (colorA, colorB, amount) => {
    const [rA, gA, bA] = colorA.match(/\w\w/g).map((c) => parseInt(c, 16));
    const [rB, gB, bB] = colorB.match(/\w\w/g).map((c) => parseInt(c, 16));
    const r = Math.round(rA + (rB - rA) * amount).toString(16).padStart(2, '0');
    const g = Math.round(gA + (gB - gA) * amount).toString(16).padStart(2, '0');
    const b = Math.round(bA + (bB - bA) * amount).toString(16).padStart(2, '0');
    return '#' + r + g + b;
}

const getWidth = (selector) => {
    const wrapper = document.querySelector(selector)
    const wrapperComputedStyle = window.getComputedStyle(wrapper, null)

    return wrapper.clientWidth -
        parseFloat(wrapperComputedStyle.paddingLeft) -
        parseFloat(wrapperComputedStyle.paddingRight)
}

const getHeight = (selector) => {
    const wrapper = document.querySelector(selector)
    const wrapperComputedStyle = window.getComputedStyle(wrapper, null)

    return wrapper.clientHeight -
        parseFloat(wrapperComputedStyle.paddingBottom) -
        parseFloat(wrapperComputedStyle.paddingTop)
}

/**
* 如果資料為 undefine , null 或空白，就回傳空字串
* @param {*} value
*/
const checkValue = (value) => {
    if (!isNullOrEmpty(value)) {
        return value
    }
    return ''
}

/**
 * 判斷是否為 undefine 或 NULL 或空白
 * @param {*} value
 */
const isNullOrEmpty = (value) => {
    if (value === undefined) {
        return true
    } else if (value === null) {
        return true
    } else if (value === '') {
        return true
    } else {
        return false
    }
}

const isActiveSelect = (selected) => {
    if (isNullOrEmpty(selected)) {
        return null;
    }
    else if (selected === 'true') {
        return true;
    }
    else {
        return false;
    }
}

const createRow = (row, index, data) => {
    const textNode = document.createTextNode(data)
    row.insertCell(index).appendChild(textNode)
}

/**
* 日期格式轉YYYY-MM-DD
* 非日期則回傳空字串
* */
function formatDate(date) {
    return moment(date).isValid() ? moment(date).format('YYYY-MM-DD hh:mm:ss') : ''
}

/*
*移除重複物件
*/
function onlyUnique(value, index, self) {
    return self.indexOf(value) === index
}

/**
* 錯誤處理
* 500: 系統異常
* 401: 權限不足
* 440: 登入逾時
* */
function errorHandler(xhr) {
    if (xhr.status === 500) {
        swal({
            title: '系統異常',
            text: '請洽系統管理員',
            type: 'warning',
            confirmButtonText: '確認'
        })
    } else if (xhr.status === 401) {
        swal({
            title: '權限不足',
            text: '拒絕存取',
            type: 'warning',
            confirmButtonText: '確認'
        })
    } else if (xhr.status === 440) {
        swal({
            title: '登入逾時',
            text: '登入逾時，請重新登入',
            type: 'warning',
            confirmButtonText: '確認'
        }, function () {
            window.location.href = `${rootPath}Account/Logout`;
        })
    } else {
        swal({
            title: '系統異常',
            text: '系統發生其他問題: ' + xhr.status + '，請洽系統管理員',
            type: 'warning',
            confirmButtonText: '確認'
        })
    }
}