import React, { useEffect, useState } from 'react';
import './Word.css';

type LeftWord = {
    id: number;
    name: string;
  };

type SavedWord = {
    id: number;
    wordId: number;
    createTime: Date;
};

const Words: React.FC = () => {
    const[leftWords, setLeftWords] = useState<LeftWord[]>([]);
    const[rightWords, setRightWords] = useState<LeftWord[]>([]);
    const[searchLetter, setSearchLetter] = useState('');
    const[notification, setNotification] = useState('');

    const fetchData = async () => {
        
        const response = await fetch('https://localhost:7014/api/table/words');

        if (!response.ok) {
            console.log("fetch error: ", response);
        }
        const data: LeftWord[] = await response.json();
        setLeftWords(data);

        
    };

    useEffect(() => {
        fetchData();
      }, []);
      
    const handleSearch = async (e: { preventDefault: () => void; }) => {
      e.preventDefault();

      setNotification('');
      const response = await fetch(`https://localhost:7014/api/table/search?letter=${searchLetter}`);

      if (!response.ok) {
        console.error('Search request failed:', response);
        return;
      }

      await fetchData();

      const responseData = await response.json();
      
      setRightWords(responseData);
      setLeftWords((prevLeftWords) =>
      prevLeftWords.filter((word) => !responseData.some((responseData: { id: number; }) => responseData.id === word.id))
    );
    };
    
    const handleSave = async () => {
      const response = await fetch('https://localhost:7014/api/table/save', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(rightWords)
      });

      if (!response.ok) {
        console.error('Save request failed:', response);
        return;
      }

      setNotification('Данные сохранены');

      setRightWords([]);
    };
    
    return <>
        <p className='notificationResult'>{notification && <p>{notification}</p>}</p>
    <div className='container'>
      <div>
        <p className='countField'>Всего слов: {leftWords.length}</p>
        <div className='word-container'>
          {leftWords.map((word) => (
            <div key={word.id}>
              {word.name}
            </div>
        ))}
        </div>

      </div>
        
        <div className='change-container'>
          <div>
            <p className='fieldTitle'>Поле для поиска</p>
          <input 
            className="fieldInput"
            type="text"
            placeholder=""
            value={searchLetter}
            onChange={(e) => setSearchLetter(e.target.value)}
            maxLength={1}
          />
          </div>
        <div className='findButtonDiv'>
          <a className='findButton' href='#' onClick={handleSearch}>Найти</a>
        </div>
        <div className='saveButtonDiv'>
          <a className='saveButton' href='#' onClick={handleSave}>Сохранить</a>
        </div>
        </div>

        <div>
          <p className='countField'>Найдено: {rightWords.length}</p>
          <div className='word-container'>
            {rightWords.map((word) => (
              <div key={word.id}>
                {word.name}
              </div>
            ))}
          </div>
        </div>
      </div>
      </>;
}

export default Words