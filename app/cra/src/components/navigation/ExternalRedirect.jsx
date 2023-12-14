import React from "react";
import { Redirect, Route } from "react-router-dom";
import PropTypes from "prop-types";
import { useUserContext } from "../../contexts/UserContext";

/**
 * When the router navigates to the internal URL, this component instead redirects the browser to an external URL.
 * @param {boolean} isPrivate - used to handle authentication for external links, defaults to false.
 * @param {string} link - the external link to send the user to when the internal route is navigated to.
 * @param {string} path - the internal route that activates this component.
 * @param {boolean} exact - passed to the internal Route component, forces exact route match before activation.
 * @param {boolean} strict - passed to the internal Route component, forces strict route match before activation.
 * @param {boolean} sensitive - passed to the internal Route component, forces case sensitive route match before activation.
 * @returns <Route> component.
 */
const ExternalRedirect = (props) => {
  // first deconstruct the props
  const { isPrivate, link, ...routeProps } = props;
  // grab user details
  const { user } = useUserContext();

  // this method is used to redirect the browser to the desired URL
  const redirectBrowser = (link) => {
    window.location.href = link;
    return null;
  };

  // define the return if isPrivate is false
  const normalReturnValue = (
    <Route {...routeProps} render={() => redirectBrowser(link)} />
  );

  // define the return if isPrivate is true
  const privateReturnValue = (
    <Route
      {...routeProps}
      render={({ location }) =>
        user ? (
          redirectBrowser(link)
        ) : (
          <Redirect to={{ pathname: "/denied", state: { from: location } }} />
        )
      }
    />
  );

  // determine which value to render
  const returnValue = isPrivate ? privateReturnValue : normalReturnValue;

  // return
  return returnValue;
};

ExternalRedirect.propTypes = {
  link: PropTypes.string.isRequired,
  path: PropTypes.string.isRequired,
  isPrivate: PropTypes.bool,
  exact: PropTypes.bool,
  sensitive: PropTypes.bool,
  strict: PropTypes.bool,
};

ExternalRedirect.defaultProps = {
  isPrivate: false,
  exact: false,
  sensitive: false,
  strict: false,
};

export default ExternalRedirect;
