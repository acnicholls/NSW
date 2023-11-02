import React, { useState, useEffect } from "react";
import { useWeatherContext } from "../contexts/WeatherContext";

const About = () => {
  const { weather, getWeather } = useWeatherContext();

  getWeather();

  let displaySection = <></>;
  if (weather) {
    displaySection = (
      <>
        <div>
          <code>{JSON.stringify(weather)}</code>
        </div>
      </>
    );
  }

  return (
    <>
      <div>{"This is the about page."}</div>
      {displaySection}
    </>
  );
};

export default About;
