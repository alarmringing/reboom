
    me.sourceDir() + "audio/bigroomdrum.wav" => string filename;
    SndBuf buf => NRev rev => dac;
    filename => buf.read;
    1 => buf.rate;
    .25 => rev.mix;
    0.3 => buf.gain;
    1.5 => buf.rate;
    0 => buf.pos;

1::second => now;
