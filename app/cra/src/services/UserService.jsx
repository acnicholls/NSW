import routes from "../constants/RouteConstants";
import * as api from "./api";

const getUserInfo = async () => {
  try {
    console.log(`querying ${routes.backend.userInfo}`);
    var response = await api.apiGet(`${routes.backend.userInfo}`);
    console.log("getUserInfo:response:", response);
    return response;
  } catch (error) {
    console.log(error);
    return error.response;
  }
};

var service = { getUserInfo };

export default service;
