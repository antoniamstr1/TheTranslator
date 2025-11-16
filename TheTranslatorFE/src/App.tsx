import Navbar from "./components/Navbar";
import { useState } from "react";

import TextInput from "./components/TextInput";
import WordsBox from "./components/WordsBox";
import './style/App.css'

function App() {
  const [words, setWords] = useState<string[]>([]);
  return (
    <div className="translator-root">
      <Navbar />
      <div className="big-container">
        <TextInput onTextClick={setWords}/>
        <WordsBox words={words}/>
      </div>



    </div>
  )
}

export default App
