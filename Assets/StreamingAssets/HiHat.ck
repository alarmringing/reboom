    me.sourceDir() + "audio/hihat.wav" => string filename;
    SndBuf buf => NRev r => dac;
    filename => buf.read;

    0.15 => r.mix;

    4 => int totalBeats;
    0.5 => float beatInSec;
    1.3 => buf.rate;

1::second => now;