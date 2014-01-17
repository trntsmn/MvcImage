# What are you doing here?

You're routing a request for a file that doesn't exist through the MVC framework where you'll resize and save it to the request path. You need to output the file to the filesystem and the browser. Don't forget to send along the correct headers depending on Jpeg/png/gif.

Thanks,
-t

Sample routes
http://localhost:3032/uploads/saris/dynamic/x230/shit.jpg --> maps to mvc
http://localhost:3032/uploads/saris/dynamic/x230/linus.jpg --> on the first ever request needs to look for http://localhost:3032/uploads/saris/linus.jpg
if found will preform a proportional resize of the height and width with the height constrianed to 230px
The image uploader place images here at native resolution: http://localhost:3032/uploads/saris/linus.jpg
http://localhost:3032/uploads/saris/dynamic/230x/linus.jpg  --> proportional resize linus to 230 wide height will be what ever is proportional
http://localhost:3032/uploads/saris/dynamic/230x230/linus.jpg --> resize linus and crop him.

