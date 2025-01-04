export function read(key) {
    return window.localStorage.getItem(key);
}

export function add(key, data) {
    window.localStorage.setItem(key, data);
}

export function remove(key) {
    window.localStorage.removeItem(key);
}