import React, { useState, useEffect } from "react";
import { useWeatherContext } from "../contexts/WeatherContext";
import { Row, Col } from "react-bootstrap";

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
      <Row>
        <Col></Col>
        <Col>
          <div>{"This is the about page."}</div>
          {displaySection}
        </Col>
        <Col></Col>
      </Row>
    </>
  );
};

export default About;
