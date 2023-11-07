import axios from "axios";

axios.defaults.withCredentials = true;

const apiGet = async (url) => {
  try {
    return await axios.get(url);
  } catch (error) {
    return error.response;
  }
};

const apiPost = async (url, data) => {
  try {
    return await axios.post(url, data);
  } catch (error) {
    return error.response;
  }
};

const apiPut = async (url, data) => {
  try {
    return await axios.put(url, data);
  } catch (error) {
    return error.response;
  }
};

const apiDelete = async (url, data) => {
  try {
    return await axios.delete(url, data);
  } catch (error) {
    return error.response;
  }
};

export { apiGet, apiPost, apiPut, apiDelete };
