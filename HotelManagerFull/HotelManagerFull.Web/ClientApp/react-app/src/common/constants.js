export const URL_API = "https://localhost:5001/api/";
export const STRING_EMPTY = "";
export const NO_DATA = "Khong có dữ liệu";
export const DATE_FORMAT_DDMMYYYYHHmmss = "DD/MM/YYYY HH:mm:ss";
export const DATE_FORMAT_YYYYMMDDHHmmss = "YYYY-MM-DD HH:mm:ss";

export const loadJs = (url) => {
  const script = document.createElement("script");
  script.src = url;
  script.async = true;
  document.body.appendChild(script);
};
