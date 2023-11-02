const routes = {
  weatherForecast: `${process.env.PUBLIC_URL}/api/WeatherForecast`,
  login: `${process.env.PUBLIC_URL}/bff`,
  logout: `${process.env.PUBLIC_URL}/bff/user/logout`,
  userInfo: `${process.env.PUBLIC_URL}/bff/user/info`,
};

export default routes;
