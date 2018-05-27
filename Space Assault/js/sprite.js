
(function() {
    function Sprite(url, pos, size, speed, frames, dir, once) {
        this.pos = pos;
        this.size = size;
        this.speed = typeof speed === 'number' ? speed : 0;
        this.frames = frames;
        this._index = 0;
        this.url = url;
        this.dir = dir || 'horizontal';
        this.once = once;
        this.advanceSize = 0;
        this.lastAdvance = Date.now();
        this.sizeBack = false;
    };

    Sprite.prototype = {

        superPulse: function(dt){
            if(dt - this.lastAdvance < 200) {
                if(!this.sizeBack){
                    if(this.advanceSize < 10) this.advanceSize++;
                    else this.sizeBack = true;
                }
                else {
                    if(this.advanceSize > 0) this.advanceSize--;
                    else this.sizeBack = false;
                }
            }
        },

        update: function(dt) {
            this._index += this.speed*dt;
        },

        render: function(ctx) {
            var frame;

            if(this.speed > 0) {
                var max = this.frames.length;
                var idx = Math.floor(this._index);
                frame = this.frames[idx % max];

                if(this.once && idx >= max) {
                    this.done = true;
                    return;
                }
            }
            else {
                frame = 0;
            }


            var x = this.pos[0];
            var y = this.pos[1];

            if(this.dir == 'vertical') {
                y += frame * this.size[1];
            }
            else {
                x += frame * this.size[0];
            }

            ctx.drawImage(resources.get(this.url),
                          x+this.advanceSize/2, y+this.advanceSize/2,
                          this.size[0], this.size[1],
                          0, 0,
                          this.size[0]+this.advanceSize, this.size[1]+this.advanceSize);
        }
    };

    window.Sprite = Sprite;
})();