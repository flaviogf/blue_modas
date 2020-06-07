function $debounce(fn, delay) {
  let timeout = null;

  return function () {
    if (timeout) {
      clearTimeout(timeout);
    }

    timeout = setTimeout(() => fn.apply(this, arguments), delay);
  };
}
