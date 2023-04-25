using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectContent
{
    public bool isInput;
    public string log;

    public uint nextIdx;

    public SelectContent(bool isInput, string log, uint nextIdx)
    {
        this.isInput = isInput;
        this.log = log;
        this.nextIdx = nextIdx;
    }
}

public class SelectController : MonoBehaviour
{
    // Start is called before the first frame update
    public diologueButtonController selectDioPrefab;
    public InputFieldController inputFieldPrefab;

    public uint dioCount;
    public bool isSelect = false;

    public void Init(LogEntry logEntry,bool isSelectable, List<Image> images = null)
    {
        isSelect = true;

        //先销毁所有子物体（用处在于可以避免重复生成）
        if (transform.childCount != 0)
        {
            foreach (Transform child in this.transform)
            {
                Destroy(child.gameObject);
            }
        }

        var selectContents = LogEntryParser.GetSelectContents(logEntry.Log);

        dioCount = (uint)selectContents.Count;

        for(int i=0;i<selectContents.Count;i++)
        {
            var item = selectContents[i];

            char p = (char)('A' + i);

            if (!item.isInput)
            {
                var db = Instantiate(selectDioPrefab, transform);
                db.Init(p+": "+item.log,logEntry.Idx,item.nextIdx,isSelectable);
                if (isSelectable)
                {
                    images[i].gameObject.SetActive(true);
                    db.SetButtonImage(images[i]);
                }
            }
            else
            {
                var dbI = Instantiate(inputFieldPrefab, transform);
                dbI.Init(item.log, p + ": ", logEntry.Idx,isSelectable);
                if (isSelectable)
                {
                    images[i].gameObject.SetActive(true);
                    dbI.SetButtonImage(images[i]);
                }
            }
        }
    }

}
