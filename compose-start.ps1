# this file is where a developer will start.
# this will bring up each part of the application
# as it's own service in a compose solution
# all set up using mounts so that changes to the 
# host file system are caught and the application's
# parts are re-loaded, giving the dev real-time
# change detection

docker-compose -f docker-compose.dev.yml up