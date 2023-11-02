import React, { createContext, useContext, useState } from "react";
import PropTypes from "prop-types";
import routes from "../constants/RouteConstants";
import { useApiContext } from "./ApiContext";

const WeatherContext = createContext(null);

export function useWeatherContext() {
  return useContext(WeatherContext);
}

export function WeatherProvider({ children }) {
  const [weather, setWeather] = useState(null);
  const api = useApiContext();

  const getWeather = async () => {
    if (weather == null) {
      try {
        console.log("calling weather info api");
        var weatherInfo = await api.apiGet(routes.weatherForecast);
        console.log("response from weather info api");
        console.log("getWeather:weatherInfo:", weatherInfo);
        if (weatherInfo.status === 200) {
          console.log("weather info loaded");
          setWeather(weatherInfo.data);
        }
      } catch (error) {
        console.log(error);
        setWeather(error);
      }
    }
  };

  return (
    <WeatherContext.Provider value={{ weather, getWeather }}>
      {children}
    </WeatherContext.Provider>
  );
}

WeatherProvider.propTypes = {
  children: PropTypes.node.isRequired,
};
