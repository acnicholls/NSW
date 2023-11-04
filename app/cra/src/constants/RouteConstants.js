const routes = {
  weatherForecast: `${process.env.PUBLIC_URL}/api/WeatherForecast`,
  login: `${process.env.PUBLIC_URL}/bff`,
  logout: `${process.env.PUBLIC_URL}/bff/user/logout`,
  userInfo: `${process.env.PUBLIC_URL}/bff/user/info`,
  post: `${process.env.PUBLIC_URL}/api/Post`,
  labelText: `${process.env.PUBLIC_URL}/api/LabelText`,
  postalCode: `${process.env.PUBLIC_URL}/api/PostalCode`,
  postCategory: `${process.env.PUBLIC_URL}/api/PostCategory`,
  user: `${process.env.PUBLIC_URL}/api/User`,
};

export default routes;
